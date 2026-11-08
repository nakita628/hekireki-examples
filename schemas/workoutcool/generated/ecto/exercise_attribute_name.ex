defmodule WorkoutCool.ExerciseAttributeName do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: WorkoutCool.PrismaDateTime, autogenerate: {WorkoutCool.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          values: [WorkoutCool.ExerciseAttributeValue.t()],
          attributes: [WorkoutCool.ExerciseAttribute.t()]
        }

  schema "exercise_attribute_names" do
    field(:id, :string, primary_key: true)
    field(:name, Ecto.Enum, values: [:TYPE, :PRIMARY_MUSCLE, :SECONDARY_MUSCLE, :EQUIPMENT, :MECHANICS_TYPE])
    has_many(:values, WorkoutCool.ExerciseAttributeValue, foreign_key: :attribute_name_id, references: :id)
    has_many(:attributes, WorkoutCool.ExerciseAttribute, foreign_key: :attribute_name_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end