defmodule Check.MixProject do
  use Mix.Project

  # `mix compile --warnings-as-errors` expands the generated `use Ecto.Schema` / schema/2 DSL in
  # lib/*.ex against the real Ecto API.
  def project do
    [
      app: :check,
      version: "0.0.0",
      elixir: "~> 1.14",
      deps: deps()
    ]
  end

  def application, do: []

  # Ecto 3.14+ is needed for UUIDv7 autogeneration
  # (@primary_key {:id, Ecto.UUID, autogenerate: [version: 7]}).
  defp deps do
    [
      {:ecto, "== 3.14.1"},
      {:ecto_sql, "== 3.14.0"},
      {:ecto_ulid_next, "== 1.0.2"}
    ]
  end
end
