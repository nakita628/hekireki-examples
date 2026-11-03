defmodule Formbricks.ContactAttribute do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          value: String.t(),
          value_number: float() | nil,
          value_date: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          attribute_key: Formbricks.ContactAttributeKey.t() | nil,
          contact: Formbricks.Contact.t() | nil
        }

  schema "ContactAttribute" do
    field(:id, :string, primary_key: true)
    field(:value, :string)
    field(:value_number, :float, source: :valueNumber)
    field(:value_date, Formbricks.PrismaDateTime, source: :valueDate)
    field(:attribute_key_id, :string, source: :attributeKeyId)
    field(:contact_id, :string, source: :contactId)
    belongs_to(:attribute_key, Formbricks.ContactAttributeKey, foreign_key: :attribute_key_id, define_field: false, type: :string)
    belongs_to(:contact, Formbricks.Contact, foreign_key: :contact_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end