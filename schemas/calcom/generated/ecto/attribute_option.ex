defmodule Calcom.AttributeOption do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          value: String.t(),
          slug: String.t(),
          is_group: boolean(),
          contains: [String.t()],
          attribute: Calcom.Attribute.t() | nil,
          assigned_users: [Calcom.AttributeToUser.t()]
        }

  schema "AttributeOption" do
    field(:value, :string)
    field(:slug, :string)
    field(:is_group, :boolean, default: false, source: :isGroup)
    field(:contains, {:array, :string})
    field(:attribute_id, :binary_id, source: :attributeId)
    belongs_to(:attribute, Calcom.Attribute, foreign_key: :attribute_id, define_field: false)
    has_many(:assigned_users, Calcom.AttributeToUser, foreign_key: :attribute_option_id)
  end
end