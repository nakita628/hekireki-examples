defmodule SuperProductivity.CaptureItem do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          grant_id: String.t(),
          ciphertext: String.t(),
          task_id: String.t(),
          created_at: integer(),
          lease_device_id: String.t() | nil,
          lease_until: integer() | nil,
          user: SuperProductivity.User.t() | nil
        }

  schema "capture_items" do
    field(:id, :string, primary_key: true)
    field(:grant_id, :string)
    field(:ciphertext, :string)
    field(:task_id, :string)
    field(:created_at, :integer)
    field(:lease_device_id, :string)
    field(:lease_until, :integer)
    belongs_to(:user, SuperProductivity.User, foreign_key: :user_id)
  end
end