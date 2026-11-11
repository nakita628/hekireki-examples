defmodule LiteLLM.LiteLLMShadowEvalJob do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          group_id: String.t(),
          target_type: String.t(),
          target_id: String.t(),
          router_name: String.t(),
          router_names: [String.t()],
          models: [String.t()],
          direction: String.t(),
          baseline_model: String.t() | nil,
          judge_model: String.t(),
          shadow_percentage: float(),
          max_turns: integer(),
          max_budget: float() | nil,
          created_by: String.t() | nil,
          ends_at: DateTime.t(),
          stopped_at: DateTime.t() | nil,
          stopped_by: String.t() | nil,
          inserted_at: DateTime.t()
        }

  schema "LiteLLM_ShadowEvalJob" do
    field(:id, :string, primary_key: true)
    field(:group_id, :string)
    field(:target_type, :string, default: "key")
    field(:target_id, :string)
    field(:router_name, :string)
    field(:router_names, {:array, :string})
    field(:models, {:array, :string})
    field(:direction, :string, default: "forward")
    field(:baseline_model, :string)
    field(:judge_model, :string)
    field(:shadow_percentage, :float)
    field(:max_turns, :integer)
    field(:max_budget, :float)
    field(:created_by, :string)
    field(:ends_at, LiteLLM.PrismaDateTime)
    field(:stopped_at, LiteLLM.PrismaDateTime)
    field(:stopped_by, :string)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end