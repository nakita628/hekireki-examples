defmodule WorkoutCool.Exercise do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: WorkoutCool.PrismaDateTime, autogenerate: {WorkoutCool.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          name_en: String.t() | nil,
          description: String.t() | nil,
          description_en: String.t() | nil,
          full_video_url: String.t() | nil,
          full_video_image_url: String.t() | nil,
          introduction: String.t() | nil,
          introduction_en: String.t() | nil,
          slug: String.t() | nil,
          slug_en: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          attributes: [WorkoutCool.ExerciseAttribute.t()],
          workout_session_exercise: [WorkoutCool.WorkoutSessionExercise.t()],
          program_session_exercise: [WorkoutCool.ProgramSessionExercise.t()],
          favorites_by_users: [WorkoutCool.UserFavoriteExercise.t()]
        }

  schema "exercises" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:name_en, :string, source: :nameEn)
    field(:description, :string)
    field(:description_en, :string, source: :descriptionEn)
    field(:full_video_url, :string, source: :fullVideoUrl)
    field(:full_video_image_url, :string, source: :fullVideoImageUrl)
    field(:introduction, :string)
    field(:introduction_en, :string, source: :introductionEn)
    field(:slug, :string)
    field(:slug_en, :string, source: :slugEn)
    has_many(:attributes, WorkoutCool.ExerciseAttribute, foreign_key: :exercise_id, references: :id)
    has_many(:workout_session_exercise, WorkoutCool.WorkoutSessionExercise, foreign_key: :exercise_id, references: :id)
    has_many(:program_session_exercise, WorkoutCool.ProgramSessionExercise, foreign_key: :exercise_id, references: :id)
    has_many(:favorites_by_users, WorkoutCool.UserFavoriteExercise, foreign_key: :exercise_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end