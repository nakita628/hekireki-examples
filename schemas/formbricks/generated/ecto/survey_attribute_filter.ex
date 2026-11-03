defmodule Formbricks.SurveyAttributeFilter do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          condition: atom(),
          value: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          attribute_key: Formbricks.ContactAttributeKey.t() | nil,
          survey: Formbricks.Survey.t() | nil
        }

  schema "SurveyAttributeFilter" do
    field(:id, :string, primary_key: true)
    field(:condition, Ecto.Enum, values: [:equals, :notEquals])
    field(:value, :string)
    field(:attribute_key_id, :string, source: :attributeKeyId)
    field(:survey_id, :string, source: :surveyId)
    belongs_to(:attribute_key, Formbricks.ContactAttributeKey, foreign_key: :attribute_key_id, define_field: false, type: :string)
    belongs_to(:survey, Formbricks.Survey, foreign_key: :survey_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end