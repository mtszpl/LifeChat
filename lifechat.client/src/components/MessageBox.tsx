import { Box, Typography, useTheme } from '@mui/material';
import * as React from 'react';
import Message from '../model/Message';
import { useSelector } from 'react-redux';

export interface IMessageBoxProps {
  message: Message
}

export function MessageBox (props: IMessageBoxProps) {
    const theme: Theme = useTheme()

    const stateUser = useSelector(state => state.user)

  return (
    <Box width="95%" paddingY="1vh" paddingX="1vw" borderRadius="12px" display="flex" flexDirection="column" marginX="auto" bgcolor={theme.palette.background.default} minHeight="2vh">
      <Typography color={theme.palette.primary.light} marginBottom="1vh" alignSelf={stateUser.name === props.message.author.username ? "flex-start" : "flex-end"}>
        {props.message.author.username}
      </Typography>
      <Typography alignSelf="flex-start">
        {props.message.content}
      </Typography>
    </Box>
  );
}
