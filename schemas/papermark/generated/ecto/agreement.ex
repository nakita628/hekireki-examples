defmodule Papermark.Agreement do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          content: String.t(),
          content_type: String.t(),
          signing_provider: String.t(),
          signing_external_id: String.t() | nil,
          signing_envelope_id: String.t() | nil,
          signing_template_id: String.t() | nil,
          require_name: boolean(),
          deleted_at: DateTime.t() | nil,
          deleted_by: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          team: Papermark.Team.t() | nil,
          links: [Papermark.Link.t()],
          responses: [Papermark.AgreementResponse.t()]
        }

  schema "Agreement" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:content, :string)
    field(:content_type, :string, default: "LINK", source: :contentType)
    field(:signing_provider, :string, default: "LEGACY", source: :signingProvider)
    field(:signing_external_id, :string, source: :signingExternalId)
    field(:signing_envelope_id, :string, source: :signingEnvelopeId)
    field(:signing_template_id, :string, source: :signingTemplateId)
    field(:require_name, :boolean, default: true, source: :requireName)
    field(:deleted_at, Papermark.PrismaDateTime, source: :deletedAt)
    field(:deleted_by, :string, source: :deletedBy)
    field(:team_id, :string, source: :teamId)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    has_many(:links, Papermark.Link, foreign_key: :agreement_id, references: :id)
    has_many(:responses, Papermark.AgreementResponse, foreign_key: :agreement_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end