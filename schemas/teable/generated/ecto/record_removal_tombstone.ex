defmodule Teable.RecordRemovalTombstone do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          table_id: String.t(),
          record_id: String.t(),
          type: String.t(),
          created_time: DateTime.t()
        }

  schema "record_removal_tombstone" do
    field(:id, :string, primary_key: true)
    field(:table_id, :string)
    field(:record_id, :string)
    field(:type, :string)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
  end
end