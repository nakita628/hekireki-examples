defmodule WorkoutCool.UserSessionProgress do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          started_at: DateTime.t(),
          completed_at: DateTime.t() | nil,
          enrollment: WorkoutCool.UserProgramEnrollment.t() | nil,
          session: WorkoutCool.ProgramSession.t() | nil,
          workout_session: WorkoutCool.WorkoutSession.t() | nil
        }

  schema "user_session_progress" do
    field(:id, :string, primary_key: true)
    field(:started_at, WorkoutCool.PrismaDateTime, autogenerate: true, source: :startedAt)
    field(:completed_at, WorkoutCool.PrismaDateTime, source: :completedAt)
    field(:enrollment_id, :string, source: :enrollmentId)
    field(:session_id, :string, source: :sessionId)
    field(:workout_session_id, :string, source: :workoutSessionId)
    belongs_to(:enrollment, WorkoutCool.UserProgramEnrollment, foreign_key: :enrollment_id, define_field: false, type: :string)
    belongs_to(:session, WorkoutCool.ProgramSession, foreign_key: :session_id, define_field: false, type: :string)
    belongs_to(:workout_session, WorkoutCool.WorkoutSession, foreign_key: :workout_session_id, define_field: false, type: :string)
  end
end