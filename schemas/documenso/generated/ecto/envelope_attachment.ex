defmodule Documenso.EnvelopeAttachment do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          type: String.t(),
          label: String.t(),
          data: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          envelope: Documenso.Envelope.t() | nil
        }

  schema "EnvelopeAttachment" do
    field(:id, :string, primary_key: true)
    field(:type, :string)
    field(:label, :string)
    field(:data, :string)
    field(:envelope_id, :string, source: :envelopeId)
    belongs_to(:envelope, Documenso.Envelope, foreign_key: :envelope_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end