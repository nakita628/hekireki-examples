defmodule WorkoutCool.Session do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          expires_at: DateTime.t(),
          token: String.t(),
          created_at: DateTime.t(),
          updated_at: DateTime.t(),
          ip_address: String.t() | nil,
          user_agent: String.t() | nil,
          impersonated_by: String.t() | nil,
          user: WorkoutCool.User.t() | nil
        }

  schema "session" do
    field(:id, :string, primary_key: true)
    field(:expires_at, WorkoutCool.PrismaDateTime, source: :expiresAt)
    field(:token, :string)
    field(:created_at, WorkoutCool.PrismaDateTime, source: :createdAt)
    field(:updated_at, WorkoutCool.PrismaDateTime, source: :updatedAt)
    field(:ip_address, :string, source: :ipAddress)
    field(:user_agent, :string, source: :userAgent)
    field(:impersonated_by, :string, source: :impersonatedBy)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, WorkoutCool.User, foreign_key: :user_id, define_field: false, type: :string)
  end
end