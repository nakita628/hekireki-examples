defmodule LiteLLM.LiteLLMSearchToolsTable do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true, source: :search_tool_id}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          search_tool_name: String.t(),
          litellm_params: map(),
          search_tool_info: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_SearchToolsTable" do
    field(:search_tool_name, :string)
    field(:litellm_params, :map)
    field(:search_tool_info, :map)
    timestamps(inserted_at_source: :created_at)
  end
end