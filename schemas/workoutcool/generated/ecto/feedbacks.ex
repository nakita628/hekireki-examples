defmodule WorkoutCool.Feedbacks do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: WorkoutCool.PrismaDateTime, autogenerate: {WorkoutCool.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          review: integer(),
          message: String.t(),
          email: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: WorkoutCool.User.t() | nil
        }

  schema "feedbacks" do
    field(:id, :string, primary_key: true)
    field(:review, :integer)
    field(:message, :string)
    field(:email, :string)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, WorkoutCool.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end