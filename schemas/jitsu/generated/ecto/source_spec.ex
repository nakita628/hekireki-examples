defmodule Jitsu.SourceSpec do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          package: String.t(),
          version: String.t(),
          specs: map() | nil,
          timestamp: DateTime.t(),
          error: String.t() | nil
        }

  schema "source_spec" do
    field(:package, :string, primary_key: true)
    field(:version, :string, primary_key: true)
    field(:specs, :map)
    field(:timestamp, Jitsu.PrismaDateTime, autogenerate: true)
    field(:error, :string)
  end
end