defmodule Formbricks.SurveyFollowUp do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          trigger: map(),
          action: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          survey: Formbricks.Survey.t() | nil
        }

  schema "SurveyFollowUp" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:trigger, :map)
    field(:action, :map)
    field(:survey_id, :string, source: :surveyId)
    belongs_to(:survey, Formbricks.Survey, foreign_key: :survey_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end