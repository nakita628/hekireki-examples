defmodule LangWatch.LangyActiveTurn do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          project_id: String.t(),
          conversation_id: String.t(),
          turn_id: String.t(),
          request_id: String.t(),
          user_id: String.t(),
          status: String.t(),
          lease_owner: String.t(),
          lease_expires_at: DateTime.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LangyActiveTurn" do
    field(:id, :string, primary_key: true)
    field(:project_id, :string, source: :projectId)
    field(:conversation_id, :string, source: :conversationId)
    field(:turn_id, :string, source: :turnId)
    field(:request_id, :string, source: :requestId)
    field(:user_id, :string, source: :userId)
    field(:status, :string)
    field(:lease_owner, :string, source: :leaseOwner)
    field(:lease_expires_at, LangWatch.PrismaDateTime, source: :leaseExpiresAt)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end