defmodule Formbricks.Language do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          code: String.t(),
          alias: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          workspace: Formbricks.Workspace.t() | nil,
          survey_languages: [Formbricks.SurveyLanguage.t()],
          creative_languages: [Formbricks.CreativeLanguage.t()]
        }

  schema "Language" do
    field(:id, :string, primary_key: true)
    field(:code, :string)
    field(:alias, :string)
    field(:workspace_id, :string, source: :workspaceId)
    belongs_to(:workspace, Formbricks.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    has_many(:survey_languages, Formbricks.SurveyLanguage, foreign_key: :language_id, references: :id)
    has_many(:creative_languages, Formbricks.CreativeLanguage, foreign_key: :language_id, references: :id)
    timestamps(inserted_at_source: :created_at)
  end
end