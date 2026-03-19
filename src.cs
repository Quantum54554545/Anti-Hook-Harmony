    public class unhook
    {
        private static List<MethodBase> methods_to_monitor = new List<MethodBase>();
        private static Harmony harmony_instance = new Harmony("unhooking");

        public static void build_method_cache()
        {
            var current_assembly = Assembly.GetExecutingAssembly();

            foreach (var type in current_assembly.GetTypes())
            {
                var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

                methods_to_monitor.AddRange(type.GetMethods(flags));
                methods_to_monitor.AddRange(type.GetConstructors(flags));
            }

            log.log_con("unhooking", $"watching {methods_to_monitor.Count} methods", ConsoleColor.Green);
        }

        public static void run_scanner()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    foreach (var method in methods_to_monitor)
                    {
                        var info = Harmony.GetPatchInfo(method);

                        if (info != null && (info.Prefixes.Count > 0 || info.Postfixes.Count > 0 || info.Transpilers.Count > 0))
                        {
                            Console.WriteLine();
                            log.log_con("unhooking", $"hook detected on {method.Name} in {method.DeclaringType.Name}!", ConsoleColor.Yellow);
                            log.log_con("unhooking", "cleaning up patches...", ConsoleColor.Cyan);

                            harmony_instance.Unpatch(method, HarmonyPatchType.All, "*");

                            log.log_con("unhooking", "method restored", ConsoleColor.Green);
                            Console.WriteLine();
                        }
                    }

                    Thread.Sleep(1000); 
                }
            });
        }
    }
