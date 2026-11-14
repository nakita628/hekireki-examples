defmodule LangWatch.ExperimentVersion do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          experiment_id: String.t(),
          project_id: String.t(),
          version: integer(),
          counter_version: integer(),
          auto_saved: boolean(),
          commit_message: String.t() | nil,
          author_id: String.t() | nil,
          author_label: String.t(),
          run_id: String.t() | nil,
          state: map(),
          schema_version: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "ExperimentVersion" do
    field(:id, :string, primary_key: true)
    field(:experiment_id, :string, source: :experimentId)
    field(:project_id, :string, source: :projectId)
    field(:version, :integer)
    field(:counter_version, :integer, source: :counterVersion)
    field(:auto_saved, :boolean, default: false, source: :autoSaved)
    field(:commit_message, :string, source: :commitMessage)
    field(:author_id, :string, source: :authorId)
    field(:author_label, :string, source: :authorLabel)
    field(:run_id, :string, source: :runId)
    field(:state, :map)
    field(:schema_version, :string, default: "1", source: :schemaVersion)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end