defmodule Jitsu.InvitationToken do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Jitsu.PrismaDateTime, autogenerate: {Jitsu.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          workspace_id: String.t(),
          email: String.t(),
          token: String.t(),
          used_by: String.t() | nil,
          role: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "InvitationToken" do
    field(:id, :string, primary_key: true)
    field(:workspace_id, :string, source: :workspaceId)
    field(:email, :string)
    field(:token, :string)
    field(:used_by, :string, source: :usedBy)
    field(:role, Ecto.Enum, values: [:owner, :editor, :analyst], default: :owner)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end