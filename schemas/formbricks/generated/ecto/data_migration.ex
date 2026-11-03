defmodule Formbricks.DataMigration do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          started_at: DateTime.t(),
          finished_at: DateTime.t() | nil,
          name: String.t(),
          status: atom()
        }

  schema "DataMigration" do
    field(:id, :string, primary_key: true)
    field(:started_at, Formbricks.PrismaDateTime, autogenerate: true)
    field(:finished_at, Formbricks.PrismaDateTime)
    field(:name, :string)
    field(:status, Ecto.Enum, values: [:pending, :applied, :failed])
  end
end