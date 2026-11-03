defmodule Documenso.Signature do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          created: DateTime.t(),
          signature_image_as_base64: String.t() | nil,
          typed_signature: String.t() | nil,
          recipient: Documenso.Recipient.t() | nil,
          field: Documenso.Field.t() | nil
        }

  schema "Signature" do
    field(:created, Documenso.PrismaDateTime, autogenerate: true)
    field(:signature_image_as_base64, :string, source: :signatureImageAsBase64)
    field(:typed_signature, :string, source: :typedSignature)
    field(:recipient_id, :id, source: :recipientId)
    field(:field_id, :id, source: :fieldId)
    belongs_to(:recipient, Documenso.Recipient, foreign_key: :recipient_id, define_field: false)
    belongs_to(:field, Documenso.Field, foreign_key: :field_id, define_field: false)
  end
end