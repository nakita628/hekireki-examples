defmodule Teable.RecordTrash do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          table_id: String.t(),
          record_id: String.t(),
          snapshot: String.t(),
          created_time: DateTime.t(),
          created_by: String.t(),
          reason: String.t(),
          record_created_time: DateTime.t() | nil,
          record_created_by: String.t() | nil,
          record_last_modified_time: DateTime.t() | nil,
          record_last_modified_by: String.t() | nil,
          operation_id: String.t() | nil
        }

  schema "record_trash" do
    field(:id, :string, primary_key: true)
    field(:table_id, :string)
    field(:record_id, :string)
    field(:snapshot, :string)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:created_by, :string)
    field(:reason, :string, default: "deleted")
    field(:record_created_time, Teable.PrismaDateTime)
    field(:record_created_by, :string)
    field(:record_last_modified_time, Teable.PrismaDateTime)
    field(:record_last_modified_by, :string)
    field(:operation_id, :string)
  end
end