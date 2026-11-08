defmodule WorkoutCool.UserProgramEnrollment do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          enrolled_at: DateTime.t(),
          current_week: integer(),
          current_session: integer(),
          completed_sessions: integer(),
          is_active: boolean(),
          completed_at: DateTime.t() | nil,
          user: WorkoutCool.User.t() | nil,
          program: WorkoutCool.Program.t() | nil,
          session_progress: [WorkoutCool.UserSessionProgress.t()]
        }

  schema "user_program_enrollments" do
    field(:id, :string, primary_key: true)
    field(:enrolled_at, WorkoutCool.PrismaDateTime, autogenerate: true, source: :enrolledAt)
    field(:current_week, :integer, default: 1, source: :currentWeek)
    field(:current_session, :integer, default: 1, source: :currentSession)
    field(:completed_sessions, :integer, default: 0, source: :completedSessions)
    field(:is_active, :boolean, default: true, source: :isActive)
    field(:completed_at, WorkoutCool.PrismaDateTime, source: :completedAt)
    field(:user_id, :string, source: :userId)
    field(:program_id, :string, source: :programId)
    belongs_to(:user, WorkoutCool.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:program, WorkoutCool.Program, foreign_key: :program_id, define_field: false, type: :string)
    has_many(:session_progress, WorkoutCool.UserSessionProgress, foreign_key: :enrollment_id, references: :id)
  end
end