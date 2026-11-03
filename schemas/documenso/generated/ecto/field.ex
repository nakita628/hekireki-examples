defmodule Documenso.Field do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          secondary_id: String.t(),
          type: atom(),
          page: integer(),
          position_x: Decimal.t(),
          position_y: Decimal.t(),
          width: Decimal.t(),
          height: Decimal.t(),
          custom_text: String.t(),
          inserted: boolean(),
          field_meta: map() | nil,
          envelope_item: Documenso.EnvelopeItem.t() | nil,
          envelope: Documenso.Envelope.t() | nil,
          recipient: Documenso.Recipient.t() | nil,
          signature: Documenso.Signature.t() | nil
        }

  schema "Field" do
    field(:secondary_id, :string, source: :secondaryId)
    field(:type, Ecto.Enum, values: [:SIGNATURE, :FREE_SIGNATURE, :INITIALS, :NAME, :EMAIL, :DATE, :TEXT, :NUMBER, :RADIO, :CHECKBOX, :DROPDOWN])
    field(:page, :integer)
    field(:position_x, :decimal, default: Decimal.new("0"), source: :positionX)
    field(:position_y, :decimal, default: Decimal.new("0"), source: :positionY)
    field(:width, :decimal, default: Decimal.new("-1"))
    field(:height, :decimal, default: Decimal.new("-1"))
    field(:custom_text, :string, source: :customText)
    field(:inserted, :boolean)
    field(:field_meta, :map, source: :fieldMeta)
    field(:envelope_item_id, :string, source: :envelopeItemId)
    field(:envelope_id, :string, source: :envelopeId)
    field(:recipient_id, :id, source: :recipientId)
    belongs_to(:envelope_item, Documenso.EnvelopeItem, foreign_key: :envelope_item_id, define_field: false, type: :string)
    belongs_to(:envelope, Documenso.Envelope, foreign_key: :envelope_id, define_field: false, type: :string)
    belongs_to(:recipient, Documenso.Recipient, foreign_key: :recipient_id, define_field: false)
    has_one(:signature, Documenso.Signature, foreign_key: :field_id)
  end
end