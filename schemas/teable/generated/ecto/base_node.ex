defmodule Teable.BaseNode do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          base_id: String.t(),
          resource_type: String.t(),
          resource_id: String.t(),
          order: float(),
          created_time: DateTime.t(),
          created_by: String.t(),
          last_modified_by: String.t() | nil,
          updated_at: DateTime.t() | nil,
          parent: Teable.BaseNode.t() | nil,
          children: [Teable.BaseNode.t()]
        }

  schema "base_node" do
    field(:id, :string, primary_key: true)
    field(:base_id, :string)
    field(:resource_type, :string)
    field(:resource_id, :string)
    field(:order, :float)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:created_by, :string)
    field(:last_modified_by, :string)
    belongs_to(:parent, Teable.BaseNode, foreign_key: :parent_id, type: :string)
    has_many(:children, Teable.BaseNode, foreign_key: :parent_id, references: :id)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end