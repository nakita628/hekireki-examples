defmodule LiteLLM.LiteLLMBackgroundInteractionSettlement do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          interaction_id: String.t(),
          custom_llm_provider: String.t(),
          create_context: map(),
          claimed_at: DateTime.t() | nil,
          claimed_by: String.t() | nil,
          settled_at: DateTime.t() | nil,
          outcome: String.t() | nil,
          inserted_at: DateTime.t()
        }

  schema "LiteLLM_BackgroundInteractionSettlement" do
    field(:interaction_id, :string, primary_key: true)
    field(:custom_llm_provider, :string)
    field(:create_context, :map)
    field(:claimed_at, LiteLLM.PrismaDateTime)
    field(:claimed_by, :string)
    field(:settled_at, LiteLLM.PrismaDateTime)
    field(:outcome, :string)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end