defmodule AnythingLLM.Invites do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          code: String.t(),
          status: String.t(),
          claimed_by: integer() | nil,
          workspace_ids: String.t() | nil,
          created_by: integer(),
          last_updated_at: DateTime.t(),
          inserted_at: DateTime.t()
        }

  schema "invites" do
    field(:code, :string)
    field(:status, :string, default: "pending")
    field(:claimed_by, :integer, source: :claimedBy)
    field(:workspace_ids, :string, source: :workspaceIds)
    field(:created_by, :integer, source: :createdBy)
    field(:last_updated_at, AnythingLLM.PrismaDateTime, autogenerate: true, source: :lastUpdatedAt)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end