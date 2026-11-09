defmodule Jitsu.SourceCatalog do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          package: String.t(),
          version: String.t(),
          key: String.t(),
          catalog: map() | nil,
          timestamp: DateTime.t(),
          status: String.t(),
          description: String.t() | nil
        }

  schema "source_catalog" do
    field(:package, :string, primary_key: true)
    field(:version, :string, primary_key: true)
    field(:key, :string, primary_key: true)
    field(:catalog, :map)
    field(:timestamp, Jitsu.PrismaDateTime, autogenerate: true)
    field(:status, :string)
    field(:description, :string)
  end
end