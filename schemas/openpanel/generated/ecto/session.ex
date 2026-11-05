defmodule OpenPanel.Session do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          token: String.t() | nil,
          expires_at: DateTime.t(),
          ip_address: String.t() | nil,
          user_agent: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: OpenPanel.User.t() | nil
        }

  schema "sessions" do
    field(:id, :string, primary_key: true)
    field(:token, :string)
    field(:expires_at, OpenPanel.PrismaDateTime, source: :expiresAt)
    field(:ip_address, :string, source: :ipAddress)
    field(:user_agent, :string, source: :userAgent)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, OpenPanel.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end