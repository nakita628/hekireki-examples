defmodule Teable.Ops do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          collection: String.t(),
          doc_id: String.t(),
          doc_type: String.t(),
          version: integer(),
          operation: String.t(),
          created_time: DateTime.t(),
          created_by: String.t()
        }

  schema "ops" do
    field(:id, :string, primary_key: true)
    field(:collection, :string)
    field(:doc_id, :string)
    field(:doc_type, :string)
    field(:version, :integer)
    field(:operation, :string)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:created_by, :string)
  end
end