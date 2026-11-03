defmodule Documenso.EmbedSignature do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          key: String.t(),
          recipient_id: integer(),
          signature: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organisation: Documenso.Organisation.t() | nil
        }

  schema "EmbedSignature" do
    field(:key, :string, primary_key: true)
    field(:recipient_id, :integer, source: :recipientId)
    field(:signature, :string)
    field(:organisation_id, :string, source: :organisationId)
    belongs_to(:organisation, Documenso.Organisation, foreign_key: :organisation_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end