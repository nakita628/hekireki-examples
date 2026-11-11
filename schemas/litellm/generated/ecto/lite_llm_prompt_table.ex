defmodule LiteLLM.LiteLLMPromptTable do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          prompt_id: String.t(),
          version: integer(),
          environment: String.t(),
          created_by: String.t() | nil,
          litellm_params: map(),
          prompt_info: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_PromptTable" do
    field(:prompt_id, :string)
    field(:version, :integer, default: 1)
    field(:environment, :string, default: "development")
    field(:created_by, :string)
    field(:litellm_params, :map)
    field(:prompt_info, :map)
    timestamps(inserted_at_source: :created_at)
  end
end