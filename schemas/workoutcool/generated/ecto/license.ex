defmodule WorkoutCool.License do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: WorkoutCool.PrismaDateTime, autogenerate: {WorkoutCool.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          key: String.t(),
          valid_from: DateTime.t(),
          valid_until: DateTime.t() | nil,
          max_users: integer() | nil,
          features: map() | nil,
          activated_at: DateTime.t() | nil,
          last_checked_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          user: WorkoutCool.User.t() | nil
        }

  schema "licenses" do
    field(:id, :string, primary_key: true)
    field(:key, :string)
    field(:valid_from, WorkoutCool.PrismaDateTime, autogenerate: true, source: :validFrom)
    field(:valid_until, WorkoutCool.PrismaDateTime, source: :validUntil)
    field(:max_users, :integer, default: 1, source: :maxUsers)
    field(:features, :map)
    field(:activated_at, WorkoutCool.PrismaDateTime, source: :activatedAt)
    field(:last_checked_at, WorkoutCool.PrismaDateTime, source: :lastCheckedAt)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, WorkoutCool.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end