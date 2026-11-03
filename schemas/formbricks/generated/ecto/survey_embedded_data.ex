defmodule Formbricks.SurveyEmbeddedData do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          workspace_id: String.t(),
          survey_id: String.t(),
          embedded_data_id: String.t(),
          storage_key: String.t(),
          order: integer()
        }

  schema "SurveyEmbeddedData" do
    field(:id, :string, primary_key: true)
    field(:workspace_id, :string, source: :workspaceId)
    field(:survey_id, :string, source: :surveyId)
    field(:embedded_data_id, :string, source: :embeddedDataId)
    field(:storage_key, :string, source: :storageKey)
    field(:order, :integer)
  end
end