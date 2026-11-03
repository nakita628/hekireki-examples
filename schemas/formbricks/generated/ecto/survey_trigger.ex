defmodule Formbricks.SurveyTrigger do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          survey: Formbricks.Survey.t() | nil,
          action_class: Formbricks.ActionClass.t() | nil
        }

  schema "SurveyTrigger" do
    field(:id, :string, primary_key: true)
    field(:survey_id, :string, source: :surveyId)
    field(:action_class_id, :string, source: :actionClassId)
    belongs_to(:survey, Formbricks.Survey, foreign_key: :survey_id, define_field: false, type: :string)
    belongs_to(:action_class, Formbricks.ActionClass, foreign_key: :action_class_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end