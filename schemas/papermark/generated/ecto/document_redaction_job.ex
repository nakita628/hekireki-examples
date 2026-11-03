defmodule Papermark.DocumentRedactionJob do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          status: String.t(),
          custom_terms: [String.t()],
          reasons: [String.t()],
          result_version_id: String.t() | nil,
          error: String.t() | nil,
          detect_run_id: String.t() | nil,
          apply_run_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          document: Papermark.Document.t() | nil,
          document_version: Papermark.DocumentVersion.t() | nil,
          team: Papermark.Team.t() | nil,
          created_by: Papermark.User.t() | nil,
          redactions: [Papermark.DocumentRedaction.t()]
        }

  schema "DocumentRedactionJob" do
    field(:id, :string, primary_key: true)
    field(:status, :string, default: "PENDING")
    field(:custom_terms, {:array, :string}, source: :customTerms)
    field(:reasons, {:array, :string})
    field(:result_version_id, :string, source: :resultVersionId)
    field(:error, :string)
    field(:detect_run_id, :string, source: :detectRunId)
    field(:apply_run_id, :string, source: :applyRunId)
    field(:document_id, :string, source: :documentId)
    field(:document_version_id, :string, source: :documentVersionId)
    field(:team_id, :string, source: :teamId)
    field(:created_by_id, :string, source: :createdById)
    belongs_to(:document, Papermark.Document, foreign_key: :document_id, define_field: false, type: :string)
    belongs_to(:document_version, Papermark.DocumentVersion, foreign_key: :document_version_id, define_field: false, type: :string)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    belongs_to(:created_by, Papermark.User, foreign_key: :created_by_id, define_field: false, type: :string)
    has_many(:redactions, Papermark.DocumentRedaction, foreign_key: :job_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end