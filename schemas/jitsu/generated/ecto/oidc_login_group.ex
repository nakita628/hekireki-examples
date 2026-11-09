defmodule Jitsu.OidcLoginGroup do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Jitsu.PrismaDateTime, autogenerate: {Jitsu.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          group_value: String.t() | nil,
          allow_all_users: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          workspace: Jitsu.Workspace.t() | nil,
          oidc_provider: Jitsu.OidcProvider.t() | nil
        }

  schema "OidcLoginGroup" do
    field(:id, :string, primary_key: true)
    field(:group_value, :string, source: :groupValue)
    field(:allow_all_users, :boolean, default: false, source: :allowAllUsers)
    field(:workspace_id, :string, source: :workspaceId)
    field(:oidc_provider_id, :string, source: :oidcProviderId)
    belongs_to(:workspace, Jitsu.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    belongs_to(:oidc_provider, Jitsu.OidcProvider, foreign_key: :oidc_provider_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end