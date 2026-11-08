defmodule WorkoutCool.ExerciseAttribute do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: WorkoutCool.PrismaDateTime, autogenerate: {WorkoutCool.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          exercise: WorkoutCool.Exercise.t() | nil,
          attribute_name: WorkoutCool.ExerciseAttributeName.t() | nil,
          attribute_value: WorkoutCool.ExerciseAttributeValue.t() | nil
        }

  schema "exercise_attributes" do
    field(:id, :string, primary_key: true)
    field(:exercise_id, :string, source: :exerciseId)
    field(:attribute_name_id, :string, source: :attributeNameId)
    field(:attribute_value_id, :string, source: :attributeValueId)
    belongs_to(:exercise, WorkoutCool.Exercise, foreign_key: :exercise_id, define_field: false, type: :string)
    belongs_to(:attribute_name, WorkoutCool.ExerciseAttributeName, foreign_key: :attribute_name_id, define_field: false, type: :string)
    belongs_to(:attribute_value, WorkoutCool.ExerciseAttributeValue, foreign_key: :attribute_value_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end