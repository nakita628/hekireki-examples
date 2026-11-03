defmodule Langfuse.AuditLog do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          type: atom(),
          api_key_id: String.t() | nil,
          user_id: String.t() | nil,
          org_id: String.t(),
          user_org_role: String.t() | nil,
          project_id: String.t() | nil,
          user_project_role: String.t() | nil,
          resource_type: String.t(),
          resource_id: String.t(),
          action: String.t(),
          before: String.t() | nil,
          after: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "audit_logs" do
    field(:id, :string, primary_key: true)
    field(:type, Ecto.Enum, values: [:USER, :API_KEY], default: :USER)
    field(:api_key_id, :string)
    field(:user_id, :string)
    field(:org_id, :string)
    field(:user_org_role, :string)
    field(:project_id, :string)
    field(:user_project_role, :string)
    field(:resource_type, :string)
    field(:resource_id, :string)
    field(:action, :string)
    field(:before, :string)
    field(:after, :string)
    timestamps(inserted_at_source: :created_at)
  end
end