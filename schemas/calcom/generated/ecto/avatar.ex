defmodule Calcom.Avatar do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          team_id: integer(),
          user_id: integer(),
          data: String.t(),
          object_key: String.t(),
          is_banner: boolean()
        }

  schema "avatars" do
    field(:team_id, :integer, default: 0, source: :teamId)
    field(:user_id, :integer, default: 0, source: :userId)
    field(:data, :string)
    field(:object_key, :string, primary_key: true, source: :objectKey)
    field(:is_banner, :boolean, default: false, source: :isBanner)
  end
end