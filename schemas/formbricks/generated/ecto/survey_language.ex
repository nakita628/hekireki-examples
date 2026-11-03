defmodule Formbricks.SurveyLanguage do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          language_id: String.t(),
          survey_id: String.t(),
          default: boolean(),
          enabled: boolean(),
          language: Formbricks.Language.t() | nil,
          survey: Formbricks.Survey.t() | nil
        }

  schema "SurveyLanguage" do
    field(:default, :boolean, default: false)
    field(:enabled, :boolean, default: true)
    field(:language_id, :string, primary_key: true, source: :languageId)
    field(:survey_id, :string, primary_key: true, source: :surveyId)
    belongs_to(:language, Formbricks.Language, foreign_key: :language_id, define_field: false, type: :string)
    belongs_to(:survey, Formbricks.Survey, foreign_key: :survey_id, define_field: false, type: :string)
  end
end