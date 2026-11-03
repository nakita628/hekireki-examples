defmodule Trigger.DataMigration do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          completed_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "DataMigration" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:completed_at, Trigger.PrismaDateTime, source: :completedAt)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end