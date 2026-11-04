defmodule Hoppscotch.MockServer do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Hoppscotch.PrismaDateTime, autogenerate: {Hoppscotch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          subdomain: String.t(),
          collection_id: String.t(),
          workspace_type: atom(),
          workspace_id: String.t(),
          delay_in_ms: integer(),
          is_public: boolean(),
          is_active: boolean(),
          hit_count: integer(),
          last_hit_at: DateTime.t() | nil,
          created_on: DateTime.t(),
          deleted_at: DateTime.t() | nil,
          updated_at: DateTime.t(),
          user: Hoppscotch.User.t() | nil,
          request_logs: [Hoppscotch.MockServerLog.t()],
          activity_history: [Hoppscotch.MockServerActivity.t()]
        }

  schema "MockServer" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:subdomain, :string)
    field(:collection_id, :string, source: :collectionID)
    field(:workspace_type, Ecto.Enum, values: [:USER, :TEAM], source: :workspaceType)
    field(:workspace_id, :string, source: :workspaceID)
    field(:delay_in_ms, :integer, default: 0, source: :delayInMs)
    field(:is_public, :boolean, default: true, source: :isPublic)
    field(:is_active, :boolean, default: true, source: :isActive)
    field(:hit_count, :integer, default: 0, source: :hitCount)
    field(:last_hit_at, Hoppscotch.PrismaDateTime, source: :lastHitAt)
    field(:created_on, Hoppscotch.PrismaDateTime, autogenerate: true, source: :createdOn)
    field(:deleted_at, Hoppscotch.PrismaDateTime, source: :deletedAt)
    field(:creator_uid, :string, source: :creatorUid)
    belongs_to(:user, Hoppscotch.User, foreign_key: :creator_uid, define_field: false, type: :string, references: :uid)
    has_many(:request_logs, Hoppscotch.MockServerLog, foreign_key: :mock_server_id, references: :id)
    has_many(:activity_history, Hoppscotch.MockServerActivity, foreign_key: :mock_server_id, references: :id)
    timestamps(inserted_at: false, updated_at_source: :updatedOn)
  end
end