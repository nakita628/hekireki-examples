defmodule Formbricks.RetentionSurveyRestore do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          restored_at: DateTime.t(),
          survey: Formbricks.Survey.t() | nil
        }

  schema "RetentionSurveyRestore" do
    field(:restored_at, Formbricks.PrismaDateTime, source: :restoredAt)
    field(:survey_id, :string, source: :surveyId)
    belongs_to(:survey, Formbricks.Survey, foreign_key: :survey_id, define_field: false, type: :string)
  end
end