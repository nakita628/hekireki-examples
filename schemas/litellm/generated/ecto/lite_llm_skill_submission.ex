defmodule LiteLLM.LiteLLMSkillSubmission do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          plugin_name: String.t(),
          operation: String.t(),
          status: String.t(),
          proposal_json: String.t(),
          archive_content: binary() | nil,
          submitted_by: String.t(),
          base_plugin_id: String.t() | nil,
          base_updated_at: DateTime.t() | nil,
          reviewed_by: String.t() | nil,
          reviewed_at: DateTime.t() | nil,
          rejection_reason: String.t() | nil,
          scan_json: String.t() | nil,
          inserted_at: DateTime.t()
        }

  schema "LiteLLM_SkillSubmission" do
    field(:plugin_name, :string)
    field(:operation, :string)
    field(:status, :string, default: "pending_review")
    field(:proposal_json, :string)
    field(:archive_content, :binary)
    field(:submitted_by, :string)
    field(:base_plugin_id, :string)
    field(:base_updated_at, LiteLLM.PrismaDateTime)
    field(:reviewed_by, :string)
    field(:reviewed_at, LiteLLM.PrismaDateTime)
    field(:rejection_reason, :string)
    field(:scan_json, :string)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end