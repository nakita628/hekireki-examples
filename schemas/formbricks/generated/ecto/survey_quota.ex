defmodule Formbricks.SurveyQuota do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          limit: integer(),
          logic: map(),
          action: atom(),
          ending_card_id: String.t() | nil,
          count_partial_submissions: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          survey: Formbricks.Survey.t() | nil,
          quota_links: [Formbricks.ResponseQuotaLink.t()]
        }

  schema "SurveyQuota" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:limit, :integer)
    field(:logic, :map, default: %{})
    field(:action, Ecto.Enum, values: [:endSurvey, :continueSurvey])
    field(:ending_card_id, :string, source: :endingCardId)
    field(:count_partial_submissions, :boolean, default: false, source: :countPartialSubmissions)
    field(:survey_id, :string, source: :surveyId)
    belongs_to(:survey, Formbricks.Survey, foreign_key: :survey_id, define_field: false, type: :string)
    has_many(:quota_links, Formbricks.ResponseQuotaLink, foreign_key: :quota_id, references: :id)
    timestamps(inserted_at_source: :created_at)
  end
end