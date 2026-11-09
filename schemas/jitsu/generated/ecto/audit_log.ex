defmodule Jitsu.AuditLog do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          timestamp: DateTime.t(),
          type: String.t(),
          severity: String.t() | nil,
          user_id: String.t() | nil,
          workspace_id: String.t() | nil,
          object_id: String.t() | nil,
          changes: map() | nil,
          auth_type: String.t() | nil,
          token_id: String.t() | nil,
          ip: String.t() | nil,
          request_headers: map() | nil
        }

  schema "AuditLog" do
    field(:id, :string, primary_key: true)
    field(:timestamp, Jitsu.PrismaDateTime, autogenerate: true)
    field(:type, :string)
    field(:severity, :string, default: "info")
    field(:user_id, :string, source: :userId)
    field(:workspace_id, :string, source: :workspaceId)
    field(:object_id, :string, source: :objectId)
    field(:changes, :map)
    field(:auth_type, :string, source: :authType)
    field(:token_id, :string, source: :tokenId)
    field(:ip, :string)
    field(:request_headers, :map, source: :requestHeaders)
  end
end