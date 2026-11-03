defmodule Teable.TableTrash do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          table_id: String.t(),
          resource_type: String.t(),
          snapshot: String.t(),
          created_time: DateTime.t(),
          created_by: String.t()
        }

  schema "table_trash" do
    field(:id, :string, primary_key: true)
    field(:table_id, :string)
    field(:resource_type, :string)
    field(:snapshot, :string)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:created_by, :string)
  end
end