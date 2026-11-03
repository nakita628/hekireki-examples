defmodule Formbricks.FeedbackSourceFormbricksMapping do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          feedback_source_id: String.t(),
          workspace_id: String.t(),
          survey_id: String.t(),
          element_id: String.t(),
          hub_field_type: atom(),
          custom_field_label: String.t() | nil,
          inserted_at: DateTime.t()
        }

  schema "FeedbackSourceFormbricksMapping" do
    field(:id, :string, primary_key: true)
    field(:feedback_source_id, :string)
    field(:workspace_id, :string, source: :workspaceId)
    field(:survey_id, :string, source: :surveyId)
    field(:element_id, :string, source: :elementId)
    field(:hub_field_type, Ecto.Enum, values: [:text, :categorical, :nps, :csat, :ces, :rating, :number, :boolean, :date], source: :hubFieldType)
    field(:custom_field_label, :string)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end