defmodule Teable.Trash do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          resource_type: String.t(),
          resource_id: String.t(),
          parent_id: String.t() | nil,
          deleted_time: DateTime.t(),
          deleted_by: String.t()
        }

  schema "trash" do
    field(:id, :string, primary_key: true)
    field(:resource_type, :string)
    field(:resource_id, :string)
    field(:parent_id, :string)
    field(:deleted_time, Teable.PrismaDateTime, autogenerate: true)
    field(:deleted_by, :string)
  end
end