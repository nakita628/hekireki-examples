defmodule Documenso.EnvelopeItem do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          title: String.t(),
          order: integer(),
          document_data: Documenso.DocumentData.t() | nil,
          envelope: Documenso.Envelope.t() | nil,
          field: [Documenso.Field.t()],
          contents: [Documenso.EnvelopeContent.t()]
        }

  schema "EnvelopeItem" do
    field(:id, :string, primary_key: true)
    field(:title, :string)
    field(:order, :integer)
    field(:document_data_id, :string, source: :documentDataId)
    field(:envelope_id, :string, source: :envelopeId)
    belongs_to(:document_data, Documenso.DocumentData, foreign_key: :document_data_id, define_field: false, type: :string)
    belongs_to(:envelope, Documenso.Envelope, foreign_key: :envelope_id, define_field: false, type: :string)
    has_many(:field, Documenso.Field, foreign_key: :envelope_item_id, references: :id)
    has_many(:contents, Documenso.EnvelopeContent, foreign_key: :envelope_item_id, references: :id)
  end
end