defmodule Documenso.EnvelopeContent do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          content_meta: map(),
          envelope_item: Documenso.EnvelopeItem.t() | nil,
          envelope: Documenso.Envelope.t() | nil,
          data_content: Documenso.DataContent.t() | nil
        }

  schema "EnvelopeContent" do
    field(:id, :string, primary_key: true)
    field(:content_meta, :map, source: :contentMeta)
    field(:envelope_item_id, :string, source: :envelopeItemId)
    field(:envelope_id, :string, source: :envelopeId)
    field(:data_content_id, :string, source: :dataContentId)
    belongs_to(:envelope_item, Documenso.EnvelopeItem, foreign_key: :envelope_item_id, define_field: false, type: :string)
    belongs_to(:envelope, Documenso.Envelope, foreign_key: :envelope_id, define_field: false, type: :string)
    belongs_to(:data_content, Documenso.DataContent, foreign_key: :data_content_id, define_field: false, type: :string)
  end
end