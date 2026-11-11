defmodule LiteLLM.LiteLLMToolTable do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true, source: :tool_id}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          tool_name: String.t(),
          origin: String.t() | nil,
          input_policy: String.t(),
          output_policy: String.t(),
          call_count: integer(),
          assignments: map() | nil,
          key_hash: String.t() | nil,
          team_id: String.t() | nil,
          key_alias: String.t() | nil,
          user_agent: String.t() | nil,
          last_used_at: DateTime.t() | nil,
          created_by: String.t() | nil,
          updated_by: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_ToolTable" do
    field(:tool_name, :string)
    field(:origin, :string)
    field(:input_policy, :string, default: "untrusted")
    field(:output_policy, :string, default: "untrusted")
    field(:call_count, :integer, default: 0)
    field(:assignments, :map, default: %{})
    field(:key_hash, :string)
    field(:team_id, :string)
    field(:key_alias, :string)
    field(:user_agent, :string)
    field(:last_used_at, LiteLLM.PrismaDateTime)
    field(:created_by, :string)
    field(:updated_by, :string)
    timestamps(inserted_at_source: :created_at)
  end
end