defmodule Documenso.TemplateDirectLink do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          token: String.t(),
          enabled: boolean(),
          direct_template_recipient_id: integer(),
          inserted_at: DateTime.t(),
          envelope: Documenso.Envelope.t() | nil
        }

  schema "TemplateDirectLink" do
    field(:id, :string, primary_key: true)
    field(:token, :string)
    field(:enabled, :boolean)
    field(:direct_template_recipient_id, :integer, source: :directTemplateRecipientId)
    field(:envelope_id, :string, source: :envelopeId)
    belongs_to(:envelope, Documenso.Envelope, foreign_key: :envelope_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end