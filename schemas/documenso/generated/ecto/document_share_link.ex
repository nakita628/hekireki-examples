defmodule Documenso.DocumentShareLink do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          email: String.t(),
          slug: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          envelope: Documenso.Envelope.t() | nil
        }

  schema "DocumentShareLink" do
    field(:email, :string)
    field(:slug, :string)
    field(:envelope_id, :string, source: :envelopeId)
    belongs_to(:envelope, Documenso.Envelope, foreign_key: :envelope_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end